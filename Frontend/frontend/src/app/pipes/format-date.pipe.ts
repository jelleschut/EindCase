import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatDate'
})
export class FormatDatePipe implements PipeTransform {

  transform(value: (Date | string | number)): string {
    return new Date(value).toLocaleDateString();
  }

}
