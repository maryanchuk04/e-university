import { Observable } from 'rxjs';

import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import { loadManager } from './state/workspace.actions';

@Component({
  selector: 'uni-workspace',
  templateUrl: './workspace.component.html',
  styleUrl: './workspace.component.scss'
})
export class WorkspaceComponent implements OnInit {
    managerLoading$:Observable<boolean>;

    constructor(private store: Store) {}

    ngOnInit() {
        this.store.dispatch(loadManager())
    }
}
