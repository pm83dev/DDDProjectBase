# DDDProjectBase

Flusso del Dato nel Progetto
Questo è il flusso del dato in un'architettura DDD con Event Sourcing, CQRS, e Hexagonal Architecture implementata nel progetto:

1. Ricezione di un Comando
Input: L'applicazione riceve un comando da un'interfaccia utente, API, o da un'altra sorgente (nel caso di un'app console, è gestito manualmente nel Program.cs).
Esempio di comando:
CreateOrder: per creare un nuovo ordine.
ShipOrder: per spedire un ordine esistente.
2. Passaggio al Command Handler
Il comando è passato a un Command Handler che è responsabile di elaborare la logica di dominio associata al comando.
Il Command Handler:
Valida il comando.
Ricostruisce lo stato corrente dell'aggregate tramite gli eventi salvati.
Esegue l'operazione richiesta, come creare o spedire un ordine.
Genera nuovi eventi in base alla logica di dominio.
Salva gli eventi generati nell'Event Store.
3. Gestione dell’Aggregate (Order)
L'aggregate Order è la rappresentazione principale della logica di dominio.
Il Command Handler utilizza l'aggregate per:
Applicare gli eventi esistenti: ricostruisce lo stato dell'aggregate riproducendo gli eventi salvati nell'Event Store.
Generare nuovi eventi: ad esempio, un OrderCreated quando un nuovo ordine viene creato o un OrderShipped quando viene spedito.
Registrare gli eventi: l'aggregate tiene traccia degli eventi generati per poi salvarli nell'Event Store.
4. Salvataggio nell'Event Store
Gli eventi generati dall'aggregate vengono salvati nell'Event Store:
Ogni evento rappresenta un cambiamento di stato immutabile.
Gli eventi sono salvati in sequenza e associati all'identificativo dell'aggregate (es. OrderId).
In questa implementazione, è utilizzato un Event Store in memoria. In un'applicazione reale, può essere implementato con un database come MongoDB o EventStoreDB.
5. Lettura del Dato tramite Query
Per leggere lo stato corrente di un ordine:
Un Query Handler recupera tutti gli eventi associati all'ordine dall'Event Store.
Gli eventi vengono applicati all'aggregate Order per ricostruire lo stato corrente.
Il Query Handler restituisce l'aggregate ricostruito o solo i dati richiesti.
6. Output del Risultato

L'applicazione restituisce i risultati delle operazioni eseguite:
Stato aggiornato dell'ordine dopo l'applicazione dei comandi.
Dettagli dell'ordine ricostruiti attraverso le query.

Flusso Completo (Esempio Pratico)
L'utente invia il comando CreateOrder con i dettagli dell'ordine (OrderId e CustomerId).

Il Command Handler:
Valida il comando.
Crea un nuovo aggregate Order e registra un evento OrderCreated.
Salva l'evento nell'Event Store.
L'utente invia il comando ShipOrder per lo stesso ordine.

Il Command Handler:
Recupera gli eventi associati a OrderId dall'Event Store.
Ricostruisce lo stato dell'ordine tramite il metodo Apply dell'aggregate.
Esegue la logica per spedire l'ordine e registra l'evento OrderShipped.
Salva l'evento OrderShipped nell'Event Store.
L'utente invia una query per leggere lo stato dell'ordine.

Il Query Handler:
Recupera tutti gli eventi associati all'ordine dall'Event Store.
Ricostruisce lo stato corrente dell'ordine tramite il metodo Apply.
Restituisce lo stato aggiornato, ad esempio: Order Shipped = True.

Diagramma del Flusso (Sintesi)
Comando → Passato al Command Handler.
Command Handler → Interagisce con l'Aggregate e genera nuovi Eventi.
Eventi → Salvati nell'Event Store.
Query → Passata al Query Handler.
Query Handler → Ricostruisce lo stato leggendo gli Eventi dall'Event Store.
Risultato → Stato ricostruito restituito all'utente.

Benefici del Flusso
Event Sourcing: Ogni modifica di stato è immutabile e registrata come evento.
CQRS: Operazioni di lettura e scrittura sono separate per ottimizzazioni e scalabilità.
Hexagonal Architecture: Il dominio è isolato da dettagli infrastrutturali, come l'Event Store.
