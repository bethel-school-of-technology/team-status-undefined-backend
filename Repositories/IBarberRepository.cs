using team_status_undefined_backend.Models;

namespace team_status_undefined_backend.Repositories;

public interface IBarberRepository
{
    IEnumerable<Barber> GetAllBarbers();
    Barber? GetBarberById(int barberId);
    Barber CreateBarber(Barber newBarber);
    Barber? UpdateBarber(Barber newBarber);
    void DeleteBarberById(int barberId);
}