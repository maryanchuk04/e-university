import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

import {
    HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../../auth/services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    private isRefreshing = false;

    constructor(private authService: AuthService, private router: Router) {}

    intercept(
        req: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        console.log('work2');
        return next.handle(req).pipe(
            catchError((error: HttpErrorResponse) => {
                if (
                    error.status === 401 &&
                    !this.isRefreshing &&
                    this.authService.isRefreshTokenExists()
                ) {
                    this.isRefreshing = true;

                    return this.authService.refreshAccessToken().pipe(
                        switchMap(() => {
                            this.isRefreshing = false;
                            const clonedRequest = req.clone({
                                headers: req.headers.set(
                                    'Authorization',
                                    `Bearer ${this.authService.getAccessToken()}`
                                ),
                            });
                            return next.handle(clonedRequest);
                        }),
                        catchError(err => {
                            this.isRefreshing = false;
                            this.authService.logout();
                            this.router.navigate(['/authenticate']);

                            return throwError(err);
                        })
                    );
                } else {
                    return throwError(error);
                }
            })
        );
    }
}
