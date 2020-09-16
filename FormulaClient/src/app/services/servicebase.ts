import { IIdprovider } from './../viewmodels/iidprovider';
import { IDataService } from './dataservice.model';
import { AuthService } from './auth.service';
import { FormulaTeam } from '../viewmodels/formulateam-model';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { AppConfig } from 'src/appconfig';

/**Base class with CRUD implementations.
 * @template TEntity the data model extending type IIdprovider
 *
 * Injectable
 */
@Injectable({
  providedIn: 'root'
})
export abstract class ServiceBase<TEntity extends IIdprovider>
      implements IDataService<TEntity> {

  private apiRoot =  AppConfig.settings.apiServer.baseUrl;
  abstract urlSegment: string; // this way, the consumer must implement the segment

  getHeaders() {
    return {
      headers: new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization',  `Bearer ${this.authSrv.getBearer()}`)
    };
  }

  getNoAuthHeader() {
    return {
      headers: new HttpHeaders()
      .set('Content-Type', 'application/json')
    };
  }
  // adding header
  // this part only works due to the authetication guard (auth is earlier than any call here)

  constructor(protected readonly http: HttpClient, protected readonly authSrv: AuthService) {
   }

  getElement(id: number): Observable<TEntity> {
    return this.http.get<TEntity>(`${this.constructUrl()}/${id}`, this.getHeaders());
  }

  createElement(entity: TEntity) {
    return this.http.post<FormulaTeam>(this.constructUrl(), JSON.stringify(entity), this.getHeaders());
  }

  updateElement(entity: TEntity) {
    return this.http.put(this.constructUrl(), entity, this.getHeaders());
  }

  deleteElement(entity: TEntity) {
    return this.http.delete<FormulaTeam>(`${this.constructUrl()}/${entity.id}`,  this.getHeaders());
  }

  getElements(): Observable<TEntity[]> {
    return this.http.get<TEntity[]>(this.constructUrl(), this.getNoAuthHeader());
  }

  protected  constructUrl(): string {
    return `${this.apiRoot}/${this.urlSegment}`;
  }

}
