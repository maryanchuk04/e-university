import { finalize, Observable } from 'rxjs';

import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import { Faculty } from '../../../core/models/faculty';
import { ManagerGatewayView } from '../../../core/models/manager-gateway-view';
import { FacultyService } from '../../../core/services/faculty.service';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { updateManagedFacultyId } from '../../state/workspace.actions';
import { selectManager } from '../../state/workspace.selectors';

@Component({
    selector: 'uni-faculty-management',
    templateUrl: './faculty-management.component.html',
})
export class FacultyManagementComponent extends BaseComponent implements OnInit {
    manager$: Observable<ManagerGatewayView>;
    faculties: Faculty[];

    currentFaculty: Faculty;

    isFacultiesLoading: boolean = true;

    constructor(private store: Store, private facultyService: FacultyService){
        super();
    }

    ngOnInit(): void {
        this.manager$ = this.store.select(selectManager);
        this.facultyService.getFaculties().pipe(finalize(() => this.isFacultiesLoading = false)).subscribe({
            next: (faculties) => {
                this.faculties = faculties;
            },
            error: () => {
                this.faculties = []
            }
        });
    }

    onFacultyChange(event: any) {
        const selectedFacultyId = event.value?.id || null;
        this.store.dispatch(updateManagedFacultyId({ facultyId: selectedFacultyId }));
    }
}
