# CubeRace

Réalisé sur Unity,
Ceci est le projet d'algorithmique dans le cadre du cours d'algorithmique 8INF870 de la session d'hiver 2017.
Il s'agit d'un jeu où le joueur doit déplacer son cube bleu vers le cube vert le plus rapidement possible.
Ce jeu met en place l'algorithme de A Star.

Pour lancer le jeu, il suffit de télécharger le projet et de lancer les exécutables MAC ou Windows selon le système d'exploitation.

La scène Game est la scène du joueur contre l'ia, tandis que la scène practice est la scène où le joueur est seul.
Dans Unity, il est possible de changer les paramètres du parcours dans l'inspecteur sur l'objet Grid. Notamment :
- Nb Line : Nombre de ligne
- Nb Column : Nombre de Colonne
- Fixed Obstacle Chance : De 1 à 100 probabilité qu'une case soit un obstacle pendant tout le jeu
- Random Obstacle Chance : De 1 à 100 probabilité qu'une case devienne un obstacle à chaque tour
- End Line : Ligne arrivée
- End Column : Colonne arrivée
