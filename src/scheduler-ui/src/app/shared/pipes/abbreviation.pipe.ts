import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'abbreviation',
})
export class AbbreviationPipe implements PipeTransform {
    transform(value: string,): string | null {
        if (!value) return null;

        return value
            .split(' ')
            .map(word => word.charAt(0))
            .join('');
    }
}
