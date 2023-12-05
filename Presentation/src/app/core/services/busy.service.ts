import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
    providedIn: 'root'
})
export class BusyService {
    private busyRequestCount = 0;
    constructor(private spinnerService: NgxSpinnerService) { }
    busy() {
        this.busyRequestCount++;
        this.spinnerService.show(undefined, {
            bdColor: "rgba(255,255,255,0.7)",
            type: "timer",
            color: "rgb(0,0,0)"
        });
    }
    idle() {
        if (this.busyRequestCount > 0) this.busyRequestCount--;
        this.spinnerService.hide();
    }
}
