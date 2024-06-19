import { Observable } from 'rxjs';

import { Injectable } from '@angular/core';

import { Faculty } from '../models/faculty';
import { BaseHttpService } from './base.service';

@Injectable({
    providedIn: 'root',
})
export class FacultyService {
    url = `api/faculty`
    constructor(private http: BaseHttpService) {}

    getFaculties(): Observable<Faculty[]> {
        return this.http.get<Faculty[]>(this.url);
    }
}
