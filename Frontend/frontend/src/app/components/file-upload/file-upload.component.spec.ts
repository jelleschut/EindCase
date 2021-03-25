import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FileUploadService } from 'src/app/services/file-upload.service';
import { FileUploadServiceStub } from 'src/app/stubs/file-uploadservicestub';
import { FileUploadComponent } from './file-upload.component';

describe('FileUploadComponent', () => {
  let sut: FileUploadComponent;
  let fixture: ComponentFixture<FileUploadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileUploadComponent ],
      providers: [{provide: FileUploadService, useClass: FileUploadServiceStub}]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FileUploadComponent);
    sut = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(sut).toBeTruthy();
  });

  it('Should call uploadservice and get result', () => {
    
    sut.submit();
    expect(sut.result).toBeTruthy();
  });

  it('Should change file', () => {
    let files: File[] = [];
    let file: File;
    files.push(file);

    sut.onFileChange(files);
    expect(sut.file).toEqual(file);
  });
});
