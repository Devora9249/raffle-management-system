import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable } from "rxjs";
import { throwError } from "rxjs/internal/observable/throwError";

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((err: HttpErrorResponse) => {
        console.error('HTTP Error:', {
          status: err.status,
          statusText: err.statusText,
          url: err.url,
          error: err.error
        });
        return throwError(() => err);
      })
    );
  }
}