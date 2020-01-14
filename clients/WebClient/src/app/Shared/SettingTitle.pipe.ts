import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
     name: 'SettingTitle'
})
export class SettingTitlePipe implements PipeTransform {

     transform(value: any, args?: any): any {
          return "[" + value + "]";
     }

}
