# Projet Relation des tables

Projet pour m'exercer sur les relation ManyToMany, OneToMany, OneToOne avec un CRUD et un front.

### Relations Many-to-Many :
- Entre Trajet et Passager, à travers l'entité de liaison `Reservation`.

### Relations Many-to-One :
- Entre Trajet et Conducteur, où un trajet appartient à un seul conducteur mais un conducteur peut avoir plusieurs trajets.
- Entre Reservation et Trajet, où une réservation est liée à un seul trajet mais un trajet peut avoir plusieurs réservations.
- Entre Reservation et Passager, où une réservation est liée à un seul passager mais un passager peut avoir plusieurs réservations.

### Relations One-to-One :
- Entre Passager et Utilisateur, où chaque passager est associé à un seul utilisateur et chaque utilisateur est associé à un seul passager.
- Entre Conducteur et Utilisateur, où chaque conducteur est associé à un seul utilisateur et chaque utilisateur est associé à un seul conducteur.
