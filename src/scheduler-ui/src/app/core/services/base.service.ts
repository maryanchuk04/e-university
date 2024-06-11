import { CookieService } from 'ngx-cookie-service';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root',
})
export class BaseHttpService {
    protected baseUrl: string = environment.gatewayBaseAddress;

    constructor(
        private http: HttpClient,
        private cookieService: CookieService
    ) {}

    protected getAuthHeaders(): HttpHeaders {
        const token = this.cookieService.get('e_access_token');

        return new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
    }

    protected get<T>(endpoint: string) {
        return this.http.get<T>(`${this.baseUrl}/${endpoint}`, {
            headers: this.getAuthHeaders(),
        });
    }

    protected post<T>(endpoint: string, body: any) {
        return this.http.post<T>(`${this.baseUrl}/${endpoint}`, body, {
            headers: this.getAuthHeaders(),
        });
    }

    protected put<T>(endpoint: string, body: any) {
        return this.http.put<T>(`${this.baseUrl}/${endpoint}`, body, {
            headers: this.getAuthHeaders(),
        });
    }

    protected delete<T>(endpoint: string) {
        return this.http.delete<T>(`${this.baseUrl}/${endpoint}`, {
            headers: this.getAuthHeaders(),
        });
    }
}
