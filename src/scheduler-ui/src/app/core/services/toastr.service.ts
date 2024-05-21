import { Injectable } from '@angular/core';

import { MessageService } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class ToastrService {
    constructor(private message: MessageService) {}

    success(summary: string, detail: string, sticky = false) {
        this.showMessage('success', summary, detail, sticky);
    }

    warning(summary: string, detail: string, sticky = false) {
        this.showMessage('warn', summary, detail, sticky);
    }

    error(summary: string, detail: string, sticky = false) {
        this.showMessage('error', summary, detail, sticky);
    }

    info(summary: string, detail: string, sticky = false) {
        this.showMessage('info', summary, detail, sticky);
    }

    black(summary: string, detail: string, sticky = false) {
        this.showMessage('contrast', summary, detail, sticky);
    }

    private showMessage(severity: string, summary: string, detail: string, sticky = false) {
        this.message.add({ severity, summary, detail, sticky })
    }
}
