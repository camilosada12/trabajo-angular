import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RolUserTableComponent } from './rol_user.component';



describe('RolUserComponent', () => {
  let component: RolUserTableComponent;
  let fixture: ComponentFixture<RolUserTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RolUserTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RolUserTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
