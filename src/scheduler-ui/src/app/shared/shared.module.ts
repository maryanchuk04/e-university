import { MessageService } from 'primeng/api';
import { AvatarModule } from 'primeng/avatar';
import { BadgeModule } from 'primeng/badge';
import { ButtonModule } from 'primeng/button';
import { ChipModule } from 'primeng/chip';
import { DividerModule } from 'primeng/divider';
import { DialogService } from 'primeng/dynamicdialog';
import { RippleModule } from 'primeng/ripple';
import { TagModule } from 'primeng/tag';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';

import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { AvatarComponent } from './components/avatar/avatar.component';
import { BaseComponent } from './components/base/base.component';
import { MenuBarComponent } from './components/menu-bar/menu-bar.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { NotificationsComponent } from './components/notifications/notifications.component';

@NgModule({
    declarations: [
        BaseComponent,
        NavMenuComponent,
        AvatarComponent,
        NotificationsComponent,
        MenuBarComponent,
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
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    providers: [MessageService, DialogService],
})
export class SharedModule {}
