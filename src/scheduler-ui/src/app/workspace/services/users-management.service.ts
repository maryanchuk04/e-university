import { Injectable } from '@angular/core';

import { BaseHttpService } from '../../core/services/base.service';

@Injectable({
    providedIn: 'root',
})
export class UsersManagementService {
    constructor(private http: BaseHttpService) {}

    createUser() {

    }
}
