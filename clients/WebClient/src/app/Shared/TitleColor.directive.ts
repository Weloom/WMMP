import { Directive, ElementRef } from '@angular/core';

@Directive({
     selector: '[mmpTitleColor]'
})
export class TitleColorDirective {

     constructor(elr: ElementRef) {
          elr.nativeElement.style.color = 'red';
     }

}
