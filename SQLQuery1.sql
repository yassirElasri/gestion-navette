create table utilissateur (id int primary key identity,
                            nom_complet varchar(30),
                            email varchar(50),
                            telephone varchar(30),
                            login varchar (30),
                            mdp varchar (30)
)
create table Ville(        id int primary key identity ,
                           nom varchar(30)

                           )
create table SocietéTransport(
                            id int primary key identity,
                            nom varchar(30),
                            email varchar(50),
                            telephone varchar(30),
                            login varchar (30),
                            mdp varchar (30)
                         
                           )
create table Autocar (      id int primary key identity,
                            id_societe int references SocietéTransport(id) ,
                            maricule varchar(50),
                            nombre_places int
                            
)
create table Navette(      id int primary key identity,
                           id_ville_depart int references Ville(id),
                           id_ville_arriver int references Ville(id),
                           heur_depart date ,
                           heur_arriver date ,
                           disponible bit ,
                           demande bit ,
                           id_car int references Autocar(id)
                          
                           )

create table Demande(      
                           id_navette int references Navette(id),
                           id_user int references utilissateur(id)

                           )

create table Abonnement(     
                           id_navette int references Navette(id),
                           id_use int references utilissateur(id)
                         

                           )


                       drop table utilissateur;
                       drop table Abonnement;
                       drop table Demande;
                       drop table Navette;
                       drop table Autocar;
                       drop table SocietéTransport;
                       drop table Ville;