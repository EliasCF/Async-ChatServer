using System.Collections.Generic;

namespace ChatServer
{
    public class IncompatibilityList
    {
        private Dictionary<ClientState, string[]> commandAndStateIncompatibility = new Dictionary<ClientState, string[]> 
        {
            { ClientState.NeedName, new string[] 
                { 
                    new CreateRoomCommand().command,
                    new JoinRoomCommand().command,
                    new LeaveRoomCommand().command,
                    new RoomOverviewCommand().command,
                    new ClientCountCommand().command
                }
            }
        };

        /// <summary>
        /// A dictionary containing all the combinations of command and ClientStates that are incompatible
        /// </summary>
        /// <value></value>
        public Dictionary<ClientState, string[]> Incompatabilies 
        { 
            get 
            { 
                return commandAndStateIncompatibility; 
            } 
        }
    }
}