import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';

import { TranslateModule } from '@ngx-translate/core';

import { MessageService } from 'primeng/api';
import { AvatarModule } from 'primeng/avatar';
import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';

import { BaseComponent } from './components/base/base.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';

@NgModule({
    declarations: [BaseComponent, NavMenuComponent],
    imports: [CommonModule, ButtonModule, TranslateModule, ToastModule, RippleModule, ToolbarModule, AvatarModule],
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
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    providers: [MessageService],
})
export class SharedModule {}
