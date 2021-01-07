create table utilissateur (id int primary key identity,
                            nom_complet varchar(30),
                            email varchar(50),
                            telephone varchar(30),
                            login varchar (30),
                            mdp varchar (30)
)
create table car (          id int primary key identity,
                            societe varchar(30),
                            maricule varchar(50),
                            nombre_places int,
                            
)
create table voyage(       
