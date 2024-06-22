import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AvatarModule } from 'primeng/avatar';
import { BadgeModule } from 'primeng/badge';
import { ButtonModule } from 'primeng/button';
import { ButtonGroupModule } from 'primeng/buttongroup';
import { ChipModule } from 'primeng/chip';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { DividerModule } from 'primeng/divider';
import { DropdownModule } from 'primeng/dropdown';
import { DialogService } from 'primeng/dynamicdialog';
import { FileUploadModule } from 'primeng/fileupload';
import { InputTextModule } from 'primeng/inputtext';
import { MenuModule } from 'primeng/menu';
import { MessagesModule } from 'primeng/messages';
import { MultiSelectModule } from 'primeng/multiselect';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { PanelModule } from 'primeng/panel';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RippleModule } from 'primeng/ripple';
import { SelectButtonModule } from 'primeng/selectbutton';
import { SidebarModule } from 'primeng/sidebar';
import { SpeedDialModule } from 'primeng/speeddial';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { TimelineModule } from 'primeng/timeline';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';

import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { AvatarComponent } from './components/avatar/avatar.component';
import { BaseComponent } from './components/base/base.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { LessonItemComponent } from './components/lesson-item/lesson-item.component';
import { MenuBarComponent } from './components/menu-bar/menu-bar.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { NotificationsComponent } from './components/notifications/notifications.component';
import { SidebarMenuComponent } from './components/sidebar-menu/sidebar-menu.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { AbbreviationPipe } from './pipes/abbreviation.pipe';
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
        SidebarMenuComponent,
        CalendarComponent,
        AbbreviationPipe,
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
        CalendarModule.forRoot({
            provide: DateAdapter,
            useFactory: adapterFactory,
        }),
        SidebarModule,
        SpeedDialModule,
        ButtonGroupModule,
        ConfirmPopupModule,
        ReactiveFormsModule,
        MultiSelectModule,
        InputTextModule,
        OverlayPanelModule,
        FileUploadModule,
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
        SidebarMenuComponent,
        CalendarComponent,
        SidebarModule,
        AvatarComponent,
        AbbreviationPipe,
        SpeedDialModule,
        ButtonGroupModule,
        ConfirmPopupModule,
        ReactiveFormsModule,
        MultiSelectModule,
        InputTextModule,
        FileUploadModule,
        OverlayPanelModule
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    providers: [MessageService, DialogService, ConfirmationService],
})
export class SharedModule {}
