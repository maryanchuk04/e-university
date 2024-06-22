import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function chnuEmailValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        const email = control.value;
        const domain = email.split('@')[1];
        return domain === 'chnu.edu.ua' ? null : { chnuEmail: true };
    };
}
