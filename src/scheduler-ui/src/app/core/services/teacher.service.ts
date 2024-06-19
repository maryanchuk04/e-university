

import { Injectable } from '@angular/core';

import { BaseHttpService } from './base.service';

@Injectable({
    providedIn: 'root',
})
export class TeacherService {
    url = `api/teacher`;

    constructor(private http: BaseHttpService) {}

    // getTeachers(): Observable<Teacher[]> {
    //     return this.http.get<Teacher[]>(this.url);
    // }
}
