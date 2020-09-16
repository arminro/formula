import { ToastrService } from 'ngx-toastr';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';


// based entirely on: https://itnext.io/handle-http-responses-with-httpinterceptor-and-toastr-in-angular-3e056759cb16
@Injectable()
export class AppHttpInterceptor implements HttpInterceptor {

  constructor(public toasterService: ToastrService)  {}


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log('REQUEST');
    console.log(req);
    const toastrConfig = { positionClass: 'toast-top-center', timeOut: 3000  };
    return next.handle(req).pipe(
      tap(evt => {
        console.log('RESPONSE')
        console.log(evt); // we just let success results go
      }),
      catchError((err: any) => {
          if (err instanceof HttpErrorResponse) {
              try {
                // validation errors (eg., 1 field omitted) arrive as an array, model errors (eg., incorrect pw) arrive as 1 obj
                    let errorTexts = '';
                    if (err.error.errors) {
                  Object.values(err.error.errors).forEach(errText => {
                    errorTexts += `\n${errText}`;
                  });
                } else {
                  errorTexts = err.error;
                }
                    this.toasterService.error(errorTexts, '', toastrConfig);
              } catch (e) {
                this.toasterService.error('An error occurred', e, toastrConfig);
              }
          }
          return of(err);
      }));

    }
  }






