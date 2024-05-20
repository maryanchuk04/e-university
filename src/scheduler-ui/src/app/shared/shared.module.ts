import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';

import { TranslateModule } from '@ngx-translate/core';

import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';

import { BaseComponent } from './components/base/base.component';

@NgModule({
    declarations: [BaseComponent],
    imports: [CommonModule, ButtonModule, TranslateModule],
    exports: [ButtonModule, TranslateModule, DividerModule, BaseComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class SharedModule {}
