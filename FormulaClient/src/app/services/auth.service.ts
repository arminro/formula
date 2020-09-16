import { AppConfig } from './../../appconfig';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { User } from '../viewmodels/user-model';
import { Login } from '../viewmodels/login-model';


/**
 * Service for authnetication.
 *
 * Injectable
 */

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  httpOptions = {
    headers: new HttpHeaders()
    .set('Content-Type', 'application/json')
  };

  private apiRoot =  AppConfig.settings.apiServer.baseUrl;  //'https://localhost:5001/api/';

  private currentUserSubject: BehaviorSubject<User>; // this will notify the other services if the user changes
  public currentUser: Observable<User>;

  constructor(private readonly http: HttpClient) {
    // default value is null for an authenticated user
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(sessionStorage.getItem('user')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

   public getBearer(): string {
    return this.currentUserValue.token;
  }

  login(candidate: Login) {
    const loginUrl = `${this.apiRoot}/${AppConfig.settings.apiServer.loginSegment}`;
    return this.http.post<User>(loginUrl, JSON.stringify(candidate), this.httpOptions)
        .pipe(
          tap((resp) => {
            if (resp && resp.token) {
              sessionStorage.setItem('user', JSON.stringify(resp));
              this.currentUserSubject.next(resp); }
            }));
}

logout() {

  const logoutUrl = `${this.apiRoot}/${AppConfig.settings.apiServer.logoutSegment}`;
  const httpLogoutOptions = {
    headers: new HttpHeaders()
    .set('Content-Type', 'application/json')
    .set('Authorization',  `Bearer ${this.getBearer()}`)
  };
  return this.http.post<any>(logoutUrl, null, httpLogoutOptions)
  .pipe(
  tap(() => {
    sessionStorage.removeItem('user');
    this.currentUserSubject.next(null);
  }));
}

}
