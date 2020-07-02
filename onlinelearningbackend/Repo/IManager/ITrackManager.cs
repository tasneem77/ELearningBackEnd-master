using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.IManager
{
    public interface ITrackManager
    {
        Track GetAllTracksByTrackId(int TrackId);
        List<Track> GetAllTracks();
        List<Track> GetTrackByTrackId(int trackId);
        Track AddTrack(Track NewTrack);
        Track EditTrack(Track EditedTrack);
        Track DeleteTrackById(int TrackId);

    }
}
