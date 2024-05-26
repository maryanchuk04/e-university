import { Component, Input } from '@angular/core';

@Component({
    selector: 'uni-avatar',
    templateUrl: './avatar.component.html',

})
export class AvatarComponent {
    @Input() image = '';
    @Input() alt = '';

    //TODO: Add sizes XL L XXL ect.
}
