using team_status_undefined_backend.Models;

namespace team_status_undefined_backend.Repositories;

public interface IBarberImgRepository
{
    IEnumerable<BarberImageLink> GetAllBarberImgs();

    BarberImageLink? CreateImg(BarberImageLink barberImage);

    void DeleteBarberImageLinkId(int barberImageLinkId);   

}
