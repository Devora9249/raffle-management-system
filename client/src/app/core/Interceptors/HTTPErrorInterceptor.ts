import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable } from "rxjs";
import { throwError } from "rxjs/internal/observable/throwError";
import { NotificationService } from "../services/notification-service";


@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private notificationService: NotificationService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((err: HttpErrorResponse) => {

        let message = 'Unexpected error occurred.';

        switch (err.status) {
          case 0:
            message = 'Cannot reach server. Check your internet connection.';
            break;

          case 400:
            message =
              err.error?.detail ||
              err.error?.title ||
              'Invalid request.';
            break;

          case 401:
            message = 'Unauthorized. Please login again.';
            break;

          case 403:
            message = 'You do not have permission to perform this action.';
            break;

          case 404:
            message = 'Requested resource was not found.';
            break;

          case 409:
            message =
              err.error?.detail ||
              'Conflict occurred.';
            break;

          case 500:
            message = 'Server error. Please try again later.';
            break;
        }

        //הודעה גלובלית
        this.notificationService.showError(message);

        //  לוג טכני בלבד
        console.error('HTTP Error:', {
          status: err.status,
          url: err.url,
          error: err.error
        });

        return throwError(() => err);
      })
    );
  }
}
