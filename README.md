# Projet Relation des tables

Projet pour m'exercer sur les relation ManyToMany, OneToMany, OneToOne avec un CRUD et un front.

### Relations many-to-many :
- Entre `Trajet` et `Passager`, à travers l'entité d'association `Reservation`.
- Entre `Passager` et `ProfilUtilisateur`, où un passager peut avoir un profil utilisateur et un profil utilisateur peut être associé à plusieurs passagers.
- Entre `Conducteur` et `ProfilUtilisateur`, où un conducteur peut avoir un profil utilisateur et un profil utilisateur peut être associé à plusieurs conducteurs.

### Relations many-to-one :
- Entre `Trajet` et `Conducteur`, où un trajet appartient à un seul conducteur mais un conducteur peut avoir plusieurs trajets.
- Entre `Reservation` et `Trajet`, où une réservation est liée à un seul trajet mais un trajet peut avoir plusieurs réservations.
- Entre `Reservation` et `Passager`, où une réservation est liée à un seul passager mais un passager peut avoir plusieurs réservations.

### Relations one-to-one :
- Entre `Passager` et `ProfilUtilisateur`, où chaque passager est associé à un seul profil utilisateur et chaque profil utilisateur est associé à un seul passager.
- Entre `Conducteur` et `ProfilUtilisateur`, où chaque conducteur est associé à un seul profil utilisateur et chaque profil utilisateur est associé à un seul conducteur.
