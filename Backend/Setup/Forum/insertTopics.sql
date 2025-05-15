insert into Topics(Title,Category,InitialMessage,MemberId)
values 
    ("Hvem kommer til kamp fredag?","Kamp","Ja, som titlen antyder. Hvem kommer til kamp i dag mod FC Bundby?",1000),
    ("Generel tråd om Fremad Amager","Sladder","Generel tråd til sladder om alt der vedrører Fremad Amager." ,1000);

insert into Posts(Content,TopicId,memberId)
values
    ("Jeg gør ihvertfald.",1,1001),
    ("Kan ikke desværre. Skal på arbejde den dag.",1,1000),
    ("Hørte lige forleden at Christian Iversen har myrdet Mikkel Andersen.",2,1002),
    ("Ja. Jeg så det ved en tilfældighed da jeg gik en tur i Englandsparken. Tog ham på fersk gerning i færd med at grave liget ned.",2,1001);
