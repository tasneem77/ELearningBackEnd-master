using Microsoft.EntityFrameworkCore;
using onlinelearningbackend.Data;
using onlinelearningbackend.Models;
using onlinelearningbackend.Repo.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.Repo.Manager
{
    public class TrackManager:ITrackManager
    {
        ApplicationDbContext db;
        public TrackManager(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        public List<Track> GetAllTracks()
        {
            var tracks = db.Tracks.FromSqlRaw<Track>($"EXEC dbo.usp_Tracks_Select ").ToList<Track>();
            return tracks;
        }
        public List<Track> GetTrackByTrackId(int trackId)
        {
            var track = db.Tracks.FromSqlRaw<Track>($"EXEC usp_Tracks_BrId {trackId}").ToList<Track>();
            return track;
        }
        public Track EditTrack(Track EditedTrack)
        {
            var track = db.Tracks.FromSqlRaw<Track>("EXEC dbo.usp_Tracks_Update {0},{1},{2}",
                EditedTrack.TrackId,
                EditedTrack.TrackName,
                EditedTrack.BranchId
                ).ToList().FirstOrDefault();
            return track;
        }
        public Track AddTrack(Track NewTrack)
        {
            var track = db.Tracks.FromSqlRaw<Track>("EXEC dbo.usp_Tracks_Insert {0},{1},{2}"
                ,NewTrack.TrackName
                ,NewTrack.BranchId
                ,NewTrack.IsActive).ToList().FirstOrDefault();
            return track;
        }
        public Track DeleteTrackById(int TrackId)
        {

         var track=db.Tracks.FromSqlRaw<Track>($"EXEC dbo.usp_Tracks_Delete {TrackId}").ToList().FirstOrDefault();
            return track;
        }

        public Track GetAllTracksByTrackId(int TrackId)
        {
            var track = db.Tracks.FromSqlRaw<Track>($"EXEC dbo.usp_Track_Track_Id {TrackId}").ToList<Track>().FirstOrDefault();
            return track;
        }
    }
}
