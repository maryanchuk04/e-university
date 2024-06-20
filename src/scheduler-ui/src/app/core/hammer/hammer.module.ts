import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { HAMMER_GESTURE_CONFIG, HammerModule } from '@angular/platform-browser';

import { CustomHammerConfig } from './hammer.config';

@NgModule({
    declarations: [],
    exports: [HammerModule],
    imports: [HammerModule, CommonModule],
    providers: [
        { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig },
    ],
})
export class CustomHammerModule {}
