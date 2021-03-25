import { FormatDatePipe } from './format-date.pipe';

describe('FormatDatePipe', () => {
  let sut = new FormatDatePipe();

  it('create an instance', () => {
    const pipe = new FormatDatePipe();
    expect(pipe).toBeTruthy();
  });

  it('Should return number as d-m-yyyy', () => {

    let expected = '25-3-2021';

    expect(sut.transform(1616671297000)).toBe(expected);
  });

  it('Should return date as d-m-yyyy', () => {

    let expected = '25-3-2021';

    expect(sut.transform(new Date('2021/03/25'))).toBe(expected);
  });

  it('Should return string as d-m-yyyy', () => {

    let expected = '25-3-2021';

    expect(sut.transform('2021/03/25')).toBe(expected);
  });
});
