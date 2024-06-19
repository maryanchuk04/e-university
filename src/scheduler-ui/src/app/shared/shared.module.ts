import { MessageService } from 'primeng/api';
import { AvatarModule } from 'primeng/avatar';
import { BadgeModule } from 'primeng/badge';
import { ButtonModule } from 'primeng/button';
import { ChipModule } from 'primeng/chip';
import { DividerModule } from 'primeng/divider';
import { DropdownModule } from 'primeng/dropdown';
import { DialogService } from 'primeng/dynamicdialog';
import { MenuModule } from 'primeng/menu';
import { MessagesModule } from 'primeng/messages';
import { PanelModule } from 'primeng/panel';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RippleModule } from 'primeng/ripple';
import { SelectButtonModule } from 'primeng/selectbutton';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { TimelineModule } from 'primeng/timeline';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';

import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { AvatarComponent } from './components/avatar/avatar.component';
import { BaseComponent } from './components/base/base.component';
import { LessonItemComponent } from './components/lesson-item/lesson-item.component';
import { MenuBarComponent } from './components/menu-bar/menu-bar.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { NotificationsComponent } from './components/notifications/notifications.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { FirstDigitPipe } from './pipes/first-digit.pipe';

@NgModule({
    declarations: [
        BaseComponent,
        NavMenuComponent,
        AvatarComponent,
        NotificationsComponent,
        MenuBarComponent,
        LessonItemComponent,
        SpinnerComponent,
        FirstDigitPipe,
    ],
    imports: [
        CommonModule,
        RouterModule,
        ButtonModule,
        TranslateModule,
        ToastModule,
        RippleModule,
        ToolbarModule,
        AvatarModule,
        BadgeModule,
        RippleModule,
        ChipModule,
        TagModule,
        SelectButtonModule,
        FormsModule,
        ProgressSpinnerModule,
        TableModule,
        PanelModule,
        TimelineModule,
        MessagesModule,
        MenuModule,
        DropdownModule,
    ],
    exports: [
        ButtonModule,
        TranslateModule,
        DividerModule,
        BaseComponent,
        NavMenuComponent,
        ToastModule,
        RippleModule,
        ToolbarModule,
        AvatarModule,
        BadgeModule,
        RippleModule,
        ChipModule,
        MenuBarComponent,
        TagModule,
        SelectButtonModule,
        FormsModule,
        ProgressSpinnerModule,
        LessonItemComponent,
        SpinnerComponent,
        TableModule,
        FirstDigitPipe,
        PanelModule,
        TimelineModule,
        MessagesModule,
        MenuModule,
        DropdownModule,
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    providers: [MessageService, DialogService],
})
export class SharedModule {}
