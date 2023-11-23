using UnityEngine;

namespace Shared.Movement
{
    public class MovementResult 
    {
        public Vector3 instantPosition;
        public bool isRunning;
        public string direction;
    }

    public class MovementHelper
    {
        /** 
        * Move a Vector3 to a position Vector3 destination with float speed
        */
        public MovementResult GoTo(Vector3 currentPosition, Vector3 movingTo, float speed)
        {
            var movementResult = new MovementResult
            {
                instantPosition = Vector3.MoveTowards(currentPosition, movingTo, speed * Time.fixedDeltaTime),
                isRunning = 
                    Mathf.FloorToInt(currentPosition.x) != Mathf.FloorToInt(movingTo.x) || 
                    Mathf.FloorToInt(currentPosition.y) != Mathf.FloorToInt(movingTo.y),
                direction = WalkingDirection(currentPosition, movingTo)
            };
            return movementResult;
        }

        public string WalkingDirection(Vector3 currentPossition, Vector3 movingTo) 
        {
            if (currentPossition.x == movingTo.x)
            {
                if(currentPossition.y == movingTo.y) return "STOP";
                else if(currentPossition.y < movingTo.y) return "N";
                else return "S";
            }
            else if (currentPossition.x > movingTo.x)
            {
                if(currentPossition.y == movingTo.y) return "W";
                else if(currentPossition.y < movingTo.y) return "NW";
                else return "SW";
            }

            if(currentPossition.y == movingTo.y) return "E";
            else if(currentPossition.y < movingTo.y) return "NE";
            else if(currentPossition.y > movingTo.y) return "SE";
            else return "SE";
        }
    }
}