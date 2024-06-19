import { Observable } from 'rxjs';

import { Injectable } from '@angular/core';

import { ManagerGatewayView } from '../models/manager-gateway-view';
import { BaseHttpService } from './base.service';

@Injectable({
    providedIn: 'root',
})
export class ManagerService {
    url = `api/manager`
    constructor(private http: BaseHttpService) {}

    getCurrentManager(): Observable<ManagerGatewayView> {
        return this.http.get<ManagerGatewayView>(this.url);
    }
}
