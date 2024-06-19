import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

import { SharedModule } from '../shared/shared.module';
import {
    FacultyManagementComponent
} from './components/faculty-management/faculty-management.component';
import {
    WorkspaceNavMenuComponent
} from './components/workspace-nav-menu/workspace-nav-menu.component';
import { WorkspaceEffects } from './state/workspace.effects';
import { workspaceReducers } from './state/workspace.reducers';
import { UsersManagementModule } from './users-management/users-management.module';
import { WorkspaceRoutingModule } from './workspace-routing.module';
import { WorkspaceComponent } from './workspace.component';

@NgModule({
    declarations: [
        WorkspaceComponent,
        WorkspaceNavMenuComponent,
        FacultyManagementComponent,
    ],
    imports: [
        CommonModule,
        UsersManagementModule,
        WorkspaceRoutingModule,
        RouterModule,
        SharedModule,
        StoreModule.forFeature('workspace', workspaceReducers),
        EffectsModule.forFeature([WorkspaceEffects]),
    ],
})
export class WorkspaceModule {}
