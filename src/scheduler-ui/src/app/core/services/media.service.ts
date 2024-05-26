import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Injectable } from '@angular/core';

export enum ScreenSize {
    XXL = 'XXL',
    XL = 'XL',
    LG = 'LG',
    MD = 'MD',
    SM = 'SM',
    XS = 'XS',
}

const SCREEN_QUERIES = {
    XXL: '(min-width: 1536px)',
    XL: '(min-width: 1280px)',
    LG: '(min-width: 1024px)',
    MD: '(min-width: 768px)',
    SM: '(min-width: 576px)',
    XS: '(min-width: 0px)',
};

@Injectable({
    providedIn: 'root',
})
export class MediaService {
    constructor(private breakpointObserver: BreakpointObserver) { }

    getScreenSize(): Observable<ScreenSize> {
        return this.breakpointObserver
            .observe(Object.values(SCREEN_QUERIES))
            .pipe(map((result) => this.mapToScreenSize(result.breakpoints)));
    }

    private mapToScreenSize(breakpoints: { [key: string]: boolean }): ScreenSize {
        if (breakpoints[SCREEN_QUERIES.XL]) {
            return ScreenSize.XL;
        } else if (breakpoints[SCREEN_QUERIES.LG]) {
            return ScreenSize.LG;
        } else if (breakpoints[SCREEN_QUERIES.MD]) {
            return ScreenSize.MD;
        } else if (breakpoints[SCREEN_QUERIES.SM]) {
            return ScreenSize.SM;
        } else {
            return ScreenSize.XS;
        }
    }
}
