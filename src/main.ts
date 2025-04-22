import { bootstrapApplication } from '@angular/platform-browser';
import { UserComponent } from './app/user/user.component';
import { appConfig } from './app/app.config';

bootstrapApplication(UserComponent, appConfig)
  .catch(err => console.error(err));
