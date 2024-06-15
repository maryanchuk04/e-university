import { CookieService } from 'ngx-cookie-service';
import { map, Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BaseHttpService } from '../../core/services/base.service';
import { AuthModel, AuthTokens } from '../models/auth.model';

@Injectable({ providedIn: 'root' })
export class AuthService extends BaseHttpService {
    url = `api/authenticate`;

    constructor(http: HttpClient, cookieService: CookieService) {
        super(http, cookieService);
    }

    authenticate(request: AuthModel): Observable<any> {
        return this.post<AuthTokens>(this.url, request).pipe(
            map(({ accessToken, refreshToken }) => {
                this.setAuthCookies({ accessToken, refreshToken });
            })
        );
    }

    refreshAccessToken(): Observable<any> {
        return this.post(
            `${
                this.url
            }/refresh-access-token?refreshToken=${this.getRefreshToken()}`,
            {}
        );
    }

    isRefreshTokenExists = (): boolean => this.getRefreshToken() != null;

    logout() {
        this.clearCookies();
    }

    getAccessToken = () => this.getToken();
}
