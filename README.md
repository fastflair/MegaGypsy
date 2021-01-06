# MegaGypsy
Mega Millions predictions using observed distributions

Is the gamblers fallicy truely false?  Reliability theory would say otherwise.  In reliability and maintainability analysis,
failures are predicted statistically on a log-log scale using CROW-AMSAA analysis and Weibull distributions.

This conflicts with the gamblers falicy because an item due for failure should fail without intervention.  This is due to material
entropic exceedence limits.  Stresses accumulate until a failure mechanism is manifested.

With the lotto, distrubutions are not truly random because numbers enter in a specific order into a specific volume with a 
specific geometry.  Numbers can appear random, but not every number has the same probability of being picked.  Think of a number
being likely to being stuck in a corner due to physics.

The concept that this works attributes luck as a thermodynamic property.  In essence, if the probability of a number being picked
does not change, and the number hasn't been picked, the number has a higher change of being picked.  The reverse is true for a 
number that has been picked frequently.  In analysing the data, the probability distributions haven't changed and numbers have
been hot and cold.  Why is this true?

Rather than assuming a random distrubution for all numbers, the observed probability distribution from past drawings are calculated 
for each ball and used to generate effective probability distrubtions.  This code is fun to analyze, as a person can virtually
generate thousands of lotto tickets without spending to see if they would have won anything.  I have yet to experience any kind of
remorse :)

For anyone that does play the lottery, I've found this algorithm increases odds of picking winning numbers.  Using random distributions,
I usually win the minumum 1/100 times as opposed to 1/20 times with this approach without real playing.

The code is a .NET solution written in C# and uses the texas lotto website to download a CSV file of the past numbers.  Only results after
Oct 28 2017 are relevant due to Mega Million Lotto structure changes.
