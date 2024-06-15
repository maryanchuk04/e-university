import { CookieService } from 'ngx-cookie-service';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root',
})
export class BaseHttpService {
    private accessTokenKey = 'e_access_token';
    private refreshTokenKey = 'e_refresh_token';
    protected baseUrl: string = environment.gatewayBaseAddress;

    constructor(
        private http: HttpClient,
        private cookieService: CookieService
    ) {}

    protected getAuthHeaders(): HttpHeaders {
        const token = this.cookieService.get(this.accessTokenKey);

        return new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
    }

    protected get<T>(endpoint: string) {
        return this.http.get<T>(`${this.baseUrl}/${endpoint}`, {
            headers: this.getAuthHeaders(),
            withCredentials: true,
        });
    }

    protected post<T>(endpoint: string, body: any) {
        return this.http.post<T>(`${this.baseUrl}/${endpoint}`, body, {
            headers: this.getAuthHeaders(),
            withCredentials: true,
        });
    }

    protected put<T>(endpoint: string, body: any) {
        return this.http.put<T>(`${this.baseUrl}/${endpoint}`, body, {
            headers: this.getAuthHeaders(),
            withCredentials: true,
        });
    }

    protected delete<T>(endpoint: string) {
        return this.http.delete<T>(`${this.baseUrl}/${endpoint}`, {
            headers: this.getAuthHeaders(),
            withCredentials: true,
        });
    }

    protected getAccessToken = () => this.cookieService.get(this.accessTokenKey);

    protected getRefreshToken = () => this.cookieService.get(this.refreshTokenKey);

    protected clearCookies = () => this.cookieService.deleteAll();
}
