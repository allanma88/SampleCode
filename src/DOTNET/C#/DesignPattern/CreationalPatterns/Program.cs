using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationalPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    public class MapSite
    {
        public virtual void Enter()
        {
            
        }
    }

    public class Room : MapSite
    {
        private int _roomNumber;
        private MapSite[] _sides = new MapSite[4];

        public Room(int roomNo)
        {
            _roomNumber = roomNo;
        }

        public MapSite GetSide(Direction direction)
        {
            throw new NotImplementedException();
        }

        public void SetSide(Direction direction, MapSite mapSite)
        {
            
        }

        public override void Enter()
        {
            
        }
    }

    public class Wall : MapSite
    {
        public Wall()
        {
            
        }

        public override void Enter()
        {
            
        }
    }

    public class Door : MapSite
    {
        private Room _room1;
        private Room _room2;
        private bool _isOpen;

        public Door(Room room1, Room room2)
        {
            _room1 = room1;
            _room2 = room2;
        }

        public override void Enter()
        {
            
        }

        public Room OtherSideFrom(Room room)
        {
            throw new NotImplementedException();
        }
    }

    public class Maze
    {
        public Maze()
        {
            
        }

        public void AddRoom(Room room)
        {
            
        }

        public Room RoomNo(int roomNo)
        {
            throw new NotImplementedException();
        }
    }

    public class MazeGame
    {
        public Maze CreateMaze()
        {
            Maze aMaze = new Maze();
            Room r1 = new Room(1);
            Room r2 = new Room(2);
            Door theDoor = new Door(r1,r2);

            aMaze.AddRoom(r1);
            aMaze.AddRoom(r2);

            r1.SetSide(Direction.North, new Wall());
            r1.SetSide(Direction.East, theDoor);
            r1.SetSide(Direction.South, new Wall());
            r1.SetSide(Direction.West, new Wall());

            r1.SetSide(Direction.North, new Wall());
            r1.SetSide(Direction.East, new Wall());
            r1.SetSide(Direction.South, new Wall());
            r1.SetSide(Direction.West, theDoor);

            return aMaze;
        }
    }
}