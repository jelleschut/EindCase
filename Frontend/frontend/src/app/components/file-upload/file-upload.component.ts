import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UpdateResult } from 'src/app/models/update-result';
import { FileUploadService } from 'src/app/services/file-upload.service';

@Component({
  selector: 'file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css'],
})
export class FileUploadComponent implements OnInit {
  @Output() updateList: EventEmitter<boolean> = new EventEmitter();

  file: File;
  validUpload = true;
  result: UpdateResult;

  constructor(private FileUploadService: FileUploadService) {}

  ngOnInit(): void {}

  onFileChange(files: File[]) {
    if (files.length > 0) {
      this.file = files[0];
    }
  }

  onUpload() {
    const formData = new FormData();
    formData.append('file', this.file);

    this.FileUploadService.upload(formData).subscribe((response) => {
      if(response) {
        this.result = response;
        this.validUpload = !response.error;
        this.updateList.emit(this.validUpload);
      }
    });
  }
}
