# GroupPhaseAssessment

Welcome, and thank you for checking out this solution to the assessment.
A few remarks are to be made about the project.

First of all, I decided to make the project in C# 6.0. The reason I did this, is because if I'm correct, Miniclip isn't on a very new version as well, so I stuck with a somewhat older framework while still being long-term support.

I tried to set up the application to be used for multiple rulesets. The only implementation I did is for a football ruleset, but since all important actors are interface implementations, adding any other rulesets shouldn't be an issue.
The  idea I had in mind, was to make a small chess implementation - considering I'm an avid chess player myself - but since I wanted to timebox myself for the project, I didn't get to that.

Speaking of timeboxing, I did spend a bit over 6 hours on this project. Most time was spent on sorting the competition participants in the correct order. Writing the LINQ query for this took at max 2 minutes, but then I got stuck on the head-to-head to determine winners.
My solution to the head-to-head issue might be a bit strange. What the code does, is that it takes the teams that would end head to head, it sets up a small new competition for these teams, takes all results that have been generated for these teams so far,
and then determines the order. This is to prevent any issues when there are more than 2 teams with the same amount of points and the same goal difference (3 is easily possible, even in a 4 team format).

I hardcoded the teams that are in the poule (to the European championship 'Poule des doods' from 2008). However, due to the structure of the application, it should be incredibly easy to put some input boxes on the form to set team titles and relative strengths. 
I didn't implement this either, unfortunately - time restraints. Furthermore, the job I'm applying for is backend, so I put less focus on features like this, and more on general structure.

I took the liberty of implementing a (very bare-bones) simulator, which determines how many attempts at a goal a team gets in the match, and how many goals they make. I took the scores of the hardcoded teams from FIFA.

I thought about testability of the solution for a while, and while I didn't implement any real tests yet, this also shouldn't be that difficult. Any kind of team or matchup could be created in the test solution that's included (but almost empty), which should make the solution properly testable.

I'm fully aware the form that the teams are displayed on is very bare-bones as well, but it functions. Click the button to see the results.

I hope the solution properly reflects my strengths at writing code, and I'd love to receive any feedback on it!

