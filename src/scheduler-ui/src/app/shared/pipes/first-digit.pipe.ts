import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'firstDigit',
})
export class FirstDigitPipe implements PipeTransform {
    transform(value: string): string {
        if (!value) return '';
        const firstDigit = value.match(/\d/);
        return firstDigit ? firstDigit[0] : '';
    }
}
