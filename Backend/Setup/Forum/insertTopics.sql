insert into Topics(Title,Category,InitialMessage,MemberId)
values
    ("Køb/Salg tråden 2025","Salg","",1000),
    ("Hvem kommer til kamp fredag?","Kamp","Ja, som titlen antyder. Hvem kommer til kamp i dag mod FC Bundby?",1000),
    ("Generel tråd om Fremad Amager","Sladder","Generel tråd til sladder om alt der vedrører Fremad Amager." ,1000);

insert into Posts(Content,TopicId,memberId)
values
    ("Tråden til køb og salg. Husk og holde priserne fair og lad være med at snyde hinanden.",1,1000),
    ("Jeg gør ihvertfald.",2,1001),
    ("Kan ikke desværre. Skal på arbejde den dag og gider ikke glo på Christian Stiversen.",2,1000),
    ("Hørte lige forleden at Christian Iversen har myrdet Mikkel Andersen.",3,1002),
    ("Ja. Jeg så det ved en tilfældighed da jeg gik en tur i Englandsparken. Tog ham på fersk gerning i færd med at grave liget ned.",3,1001);
