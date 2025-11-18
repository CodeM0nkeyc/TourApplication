import { Component } from '@angular/core';
import {SectionIntroComponent} from "./section-intro/section-intro.component";
import {SectionAboutComponent} from "./section-about/section-about.component";
import {SectionFeaturesComponent} from "./section-features/section-features.component";
import {SectionToursComponent} from "./section-tours/section-tours.component";
import {SectionReviewsComponent} from "./section-reviews/section-reviews.component";
import {SectionBookComponent} from "./section-book/section-book.component";
import {HeaderComponent, FooterComponent, GlobalErrorComponent} from "shared/components"

@Component({
    selector: 'home-root',
    standalone: true,
    imports: [
        SectionIntroComponent,
        SectionAboutComponent,
        SectionFeaturesComponent,
        SectionToursComponent,
        SectionReviewsComponent,
        SectionBookComponent,
        HeaderComponent,
        FooterComponent,
        GlobalErrorComponent
    ],
    templateUrl: './home.component.html',
    styleUrl: './home.component.scss'
})
export class HomeComponent {

}
