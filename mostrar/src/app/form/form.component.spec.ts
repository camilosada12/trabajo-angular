import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormTableComponent } from './form.component';



describe('FormComponent', () => {
  let component: FormTableComponent;
  let fixture: ComponentFixture<FormTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
