import { addDays } from 'date-fns';
import { CookieService } from 'ngx-cookie-service';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root',
})
export class BaseHttpService {
    private accessTokenKey = 'e_uni_access_token';
    private refreshTokenKey = 'e_uni_refresh_token';

    protected baseUrl: string = environment.gatewayBaseAddress;

    constructor(
        private http: HttpClient,
        private cookieService: CookieService
    ) {}

    public getAuthHeaders(): HttpHeaders {
        const token = this.cookieService.get(this.accessTokenKey);

        return new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
    }

    public get<T>(endpoint: string) {
        return this.http.get<T>(`${this.baseUrl}/${endpoint}`, {
            headers: this.getAuthHeaders(),
            withCredentials: true,
        });
    }

    public post<T>(endpoint: string, body: any) {
        return this.http.post<T>(`${this.baseUrl}/${endpoint}`, body, {
            headers: this.getAuthHeaders(),
            withCredentials: true,
        });
    }

    public put<T>(endpoint: string, body: any) {
        return this.http.put<T>(`${this.baseUrl}/${endpoint}`, body, {
            headers: this.getAuthHeaders(),
            withCredentials: true,
        });
    }

    public delete<T>(endpoint: string) {
        return this.http.delete<T>(`${this.baseUrl}/${endpoint}`, {
            headers: this.getAuthHeaders(),
            withCredentials: true,
        });
    }

    public getToken = () => this.cookieService.get(this.accessTokenKey);

    public getRefreshToken = () =>
        this.cookieService.get(this.refreshTokenKey);

    public clearCookies = () => this.cookieService.deleteAll();

    protected setAuthCookies = ({ accessToken, refreshToken }) => {
        const accessTokenCookieOptions = {
            httpOnly: false,
            secure: true,
            sameSite: 'None' as 'None',
            expires: addDays(new Date(), 1),
        };

        const refreshTokenCookieOptions = {
            httpOnly: false,
            secure: true,
            sameSite: 'None' as 'None',
            expires: addDays(new Date(), 15),
        };

        this.cookieService.set(
            this.accessTokenKey,
            accessToken,
            accessTokenCookieOptions
        );
        this.cookieService.set(
            this.refreshTokenKey,
            refreshToken,
            refreshTokenCookieOptions
        );
    };
}
