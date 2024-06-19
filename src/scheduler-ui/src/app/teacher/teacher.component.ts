import { Observable } from 'rxjs';

import { Component, OnInit } from '@angular/core';

import { Teacher } from '../core/models/teacher';
import { BaseComponent } from '../shared/components/base/base.component';

@Component({
  selector: 'uni-teacher',
  templateUrl: './teacher.component.html',
  styleUrl: './teacher.component.scss'
})
export class TeacherComponent extends BaseComponent implements OnInit {
    private teachers$: Observable<Teacher[]>;

    constructor() { super(); }

    ngOnInit(): void {

    }
}
