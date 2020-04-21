## Casshan

Casshan is an automated tool for finding and logging accounts which are suspected of automating the levelling process in League of Legends. If you're interested in the details of how it came about, I have published a writeup [here](https://jamiedavidson.github.io/2020/04/21/Identifying-black-market-accounts-in-League-of-Legends.html)

### What you'll need to run Casshan

[A **personal API key** from riot](https://developer.riotgames.com/docs/portal#web-apis_api-keys). It is **very important** that you do not use a development key for running reports if your intention is to do something with the output, like report it to Riot. Using a development key will return account IDs which look real, but do not correspond to a real account.

Casshan is in need of some configuration improvements. You'll need to modify the project to use your API key and select your region.

**An account to start off as your patient zero**. Enter a game of coop vs AI (intro bots) and find an account that looks suspicious. Set that account as `patientZero` in `Program.cs`.

More to come on this soon.